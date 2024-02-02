namespace ShopCommons.Models.Enum
{
    public enum RabbitMQMessageType
    {
        #region Ordini
        REQUEST_ORDER_DETAILS = 0,
        REQUEST_CREATE_ORDER = 1,
        REQUEST_DELETE_ORDER = 2,
        #endregion

        #region Prodotti
        REQUEST_PRODUCTS_LIST = 10,
        REQUEST_GET_PRODUCT = 11,
        REQUEST_CREATE_PRODUCT = 12,
        REQUEST_DELETE_PRODUCT = 13,
        REQUEST_EDIT_PRODUCT = 14,
        #endregion
    }
}
