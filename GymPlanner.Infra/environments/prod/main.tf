terraform {
  required_version = ">= 1.7.0"
  backend "azurerm" {}
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.90"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_service_plan" "plan" {
  name                = var.app_service_plan_name
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = "Linux"

  sku_name = "F1"
}

resource "azurerm_linux_web_app" "app" {
  name                = var.web_app_name
  location            = var.location
  resource_group_name = var.resource_group_name
  service_plan_id     = azurerm_service_plan.plan.id

  site_config {
    always_on = false
    application_stack {
      dotnet_version = var.dotnet_version
    }
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT" = "Production"
  }
}


data "azurerm_key_vault" "kv" {
  name                = "kv-gymplanner-prod"
  resource_group_name = var.resource_group_name
}

data "azurerm_key_vault_secret" "sql_password" {
  name         = "adminpassword"
  key_vault_id = data.azurerm_key_vault.kv.id
}

resource "azurerm_mssql_server" "sqlserver" {
  name                         = var.sql_server_name
  location                     = var.sql_location
  resource_group_name          = var.resource_group_name
  version                      = "12.0"
  administrator_login          = var.sql_admin
  administrator_login_password = data.azurerm_key_vault_secret.sql_password.value
}


resource "azurerm_mssql_database" "sqldb" {
  name        = var.sql_database_name
  server_id   = azurerm_mssql_server.sqlserver.id
  sku_name    = "Basic"
  max_size_gb = 1
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name             = "AllowAzureServices"
  server_id        = azurerm_mssql_server.sqlserver.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}


