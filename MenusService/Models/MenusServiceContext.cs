using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MenusService.Models
{
    public class MenusServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MenusServiceContext() : base("Data Source=SIBONGISENIB17L\\SQLEXPRESS;Initial Catalog=Menu;Integrated Security=True")
        {
        }

        public static MenusServiceContext Create()
        {
            return new MenusServiceContext();
        }
        public DbSet<Resturant> Resturants { get; set; }

        public System.Data.Entity.DbSet<MenusService.Models.Meal> Meals { get; set; }
    }
}
