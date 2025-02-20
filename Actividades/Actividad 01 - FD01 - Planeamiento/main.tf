provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "eventifyme_rg" {
  name     = "EventifyMeResourceGroup"
  location = "Brazil South"
}

resource "azurerm_service_plan" "eventifyme_plan" {
  name                = "EventifyMeServicePlan"
  location            = azurerm_resource_group.eventifyme_rg.location
  resource_group_name = azurerm_resource_group.eventifyme_rg.name
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "eventifyme" {
  name                = "eventifyme-app"
  location            = azurerm_resource_group.eventifyme_rg.location
  resource_group_name = azurerm_resource_group.eventifyme_rg.name
  service_plan_id     = azurerm_service_plan.eventifyme_plan.id

  site_config {
    always_on = true
  }
}
