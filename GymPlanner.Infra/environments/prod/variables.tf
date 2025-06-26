variable "location" {
  type = string
}

variable "resource_group_name" {
  type = string
}

variable "app_service_plan_name" {
  type = string
}

variable "web_app_name" {
  type = string
}

variable "dotnet_version" {
  type = string
}

variable "sql_server_name" {
  type = string
}

variable "sql_database_name" {
  type = string
}

variable "sql_admin" {
  type    = string
  default = "sqladmin"
}

variable "sql_location" {
  type = string

}
