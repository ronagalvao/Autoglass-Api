namespace Autoglass.Domain
{
    public class Vendor
{
    public string Code { get; }
    public string Description { get; }
    public string SupplierCnpj { get; }

    public Vendor(string code, string description, string supplierCnpj)
    {
        Code = code;
        Description = description;
        SupplierCnpj = supplierCnpj;
    }
}

}