using System;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Abstractions;

using Xamarin.Forms;
using UtahOpenSource.DataStore.Azure;

namespace UtahOpenSource.DataStore.Azure
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override string Identifier => "Category";
    }
}

