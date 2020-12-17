namespace BackEnd.Entities
{
public class Branch
{
    public int BranchID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Phone { get; set; }


    public Branch(int branchId, string name, string address, int phone)
    {
        BranchID = branchId;
        Name = name;
        Address = address;
        Phone = phone;
    }
}
}