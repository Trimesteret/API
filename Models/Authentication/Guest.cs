using API.Enums;
using API.Models.Orders;

namespace API.Models.Authentication;

public class Guest : User
{
    public List<PurchaseOrder> PurchaseOrders { get; protected set; }

    public Guest(){}

    public Guest(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Token = user.Token;
        PurchaseOrders = new List<PurchaseOrder>();
        Role = Role.Guest;
    }

    public PurchaseOrder GetOpenPurchaseOrder()
    {
        foreach (PurchaseOrder purchaseOrder in PurchaseOrders)
        {
            if (purchaseOrder.PurchaseOrderState == PurchaseOrderState.Open)
            {
                return purchaseOrder;
            }
        }

        return null;
    }

    public PurchaseOrder CreatePurchaseOrder()
    {
        var customerPurchaseOrder = GetOpenPurchaseOrder();
        if(customerPurchaseOrder != null)
        {
            return customerPurchaseOrder;
        }

        customerPurchaseOrder = new PurchaseOrder(this);
        this.PurchaseOrders.Add(customerPurchaseOrder);
        return customerPurchaseOrder;
    }
}
