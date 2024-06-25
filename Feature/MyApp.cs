using System;
using Android.App;
using Android.Runtime;
using productDemo.DI;

namespace productDemo.Feature
{
    [Application]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            DependencyConfig.Configure(this);
        }

        // public override void OnCreate()
        // {
        //     base.OnCreate();
        //     DependencyConfig.Configure(this);
        // }
    }
}