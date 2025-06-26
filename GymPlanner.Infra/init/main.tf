terraform {
  required_version = ">= 1.7.0"
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

data "azurerm_client_config" "current" {}

resource "random_string" "suffix" {
  length  = 3
  special = false
  upper   = false
  numeric = true
}

resource "azurerm_resource_group" "rg_dev" {
  name     = var.resource_group_name_dev
  location = var.location
}

resource "azurerm_resource_group" "rg_prod" {
  name     = var.resource_group_name_prod
  location = var.location
}

resource "azurerm_storage_account" "tfstategymplannerdev" {
  name                     = "tfstategymplannerdev${random_string.suffix.result}"
  resource_group_name      = azurerm_resource_group.rg_dev.name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_account" "tfstategymplannerprod" {
  name                     = "tfstategymplannerprod${random_string.suffix.result}"
  resource_group_name      = azurerm_resource_group.rg_prod.name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "tfstate_dev" {
  name                  = "tfstate"
  storage_account_name  = azurerm_storage_account.tfstategymplannerdev.name
  container_access_type = "private"
}

resource "azurerm_storage_container" "tfstate_prod" {
  name                  = "tfstate"
  storage_account_name  = azurerm_storage_account.tfstategymplannerprod.name
  container_access_type = "private"
}



resource "azurerm_key_vault" "kv_dev" {
  name                     = "kv-gymplanner-dev"
  location                 = azurerm_resource_group.rg_dev.location
  resource_group_name      = azurerm_resource_group.rg_dev.name
  tenant_id                = data.azurerm_client_config.current.tenant_id
  sku_name                 = "standard"
  purge_protection_enabled = false

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    secret_permissions = ["Get", "List", "Set"]
  }
}

resource "azurerm_key_vault" "kv_prod" {
  name                     = "kv-gymplanner-prod"
  location                 = azurerm_resource_group.rg_prod.location
  resource_group_name      = azurerm_resource_group.rg_prod.name
  tenant_id                = data.azurerm_client_config.current.tenant_id
  sku_name                 = "standard"
  purge_protection_enabled = false

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    secret_permissions = ["Get", "List", "Set"]
  }
}

resource "azurerm_key_vault_access_policy" "pipeline_access_dev" {
  key_vault_id = azurerm_key_vault.kv_dev.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = "281edbbb-f03f-456e-b9bc-32974dab295a" # id del service connection

  secret_permissions = ["Get", "List"]
}


resource "azurerm_key_vault_access_policy" "pipeline_access_prod" {
  key_vault_id = azurerm_key_vault.kv_prod.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = "ed66d9be-98d8-4667-a5db-21d69934b133" # id del service connection

  secret_permissions = ["Get", "List"]
}

