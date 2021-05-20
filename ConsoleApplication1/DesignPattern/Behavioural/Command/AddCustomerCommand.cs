using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Command
{
    public class AddCustomerCommand : fx.ICommand
    {
        private CustomerService service;
        public AddCustomerCommand(CustomerService service)
        {
            this.service = service;
        }
        public void Execute()
        {
            this.service.AddCustomer();   
        }
    }
}
