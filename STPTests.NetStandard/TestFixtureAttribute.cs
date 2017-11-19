using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace SmartThreadPoolTests
{
    class TestFixtureAttribute:Attribute
    {
    }
    class SetUpAttribute : Attribute
    {
    }
    class TearDownAttribute : Attribute
    {
    }
    class RequiresThreadAtteibute : Attribute
    {

    }
    class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string name)
        {

        }
    }
    public class NunitTestBase:IDisposable
    {
        public NunitTestBase()
        {
            GetType().GetMethods().Where(a => a.GetCustomAttribute<SetUpAttribute>() != null).SingleOrDefault()?.Invoke(this,new object[0]);
        }

        public void Dispose()
        {
            GetType().GetMethods().Where(a => a.GetCustomAttribute<TearDownAttribute>() != null).SingleOrDefault()?.Invoke(this, new object[0]);
        }
    }
}
