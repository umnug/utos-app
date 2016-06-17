using System;
using System.Data.Entity;

namespace UtahOpenSource.Backend.Models
{
    //DropCreateDatabaseIfModelChanges
    public class UtahOpenSourceContextInitializer : DropCreateDatabaseIfModelChanges<UtahOpenSourceContext>
    {
        protected override void Seed(UtahOpenSourceContext context)
        {
            //Seed Data Here
        }
    }
}
