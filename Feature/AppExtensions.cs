using System.Collections.Generic;
using Android.App;
using Android.OS;
using AndroidX.Fragment.App;
using Google.Android.Material.BottomSheet;
using Java.Lang;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace productDemo.Feature
{
    public class AppExtensions
    {
        public static void ObserveComponentCreation(Application application, IComponentCreation callback)
        {
            application.RegisterActivityLifecycleCallbacks(new ComponentCreationCallbacks(callback));
        }

        private class ComponentCreationCallbacks : Object, Application.IActivityLifecycleCallbacks
        {
            private readonly IComponentCreation _callback;
            private readonly HashSet<Fragment> _fragmentSet = new HashSet<Fragment>();

            public ComponentCreationCallbacks(IComponentCreation callback)
            {
                _callback = callback;
            }

            public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
            {
                _callback.OnActivityCreated(activity);
                RegisterFragmentLifecycleCallbacks(activity);
            }

            public void OnActivityStarted(Activity activity)
            {
            }

            public void OnActivityResumed(Activity activity)
            {
            }

            public void OnActivityPaused(Activity activity)
            {
            }

            public void OnActivityStopped(Activity activity)
            {
            }

            public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
            {
            }

            public void OnActivityDestroyed(Activity activity)
            {
            }

            private void RegisterFragmentLifecycleCallbacks(Activity activity)
            {
                if (!(activity is FragmentActivity fragmentActivity)) return;

                fragmentActivity.SupportFragmentManager.RegisterFragmentLifecycleCallbacks(
                    new FragmentCreationCallbacks(_callback, _fragmentSet),
                    true
                );
            }
        }

        private class FragmentCreationCallbacks : FragmentManager.FragmentLifecycleCallbacks
        {
            private readonly IComponentCreation _callback;
            private readonly HashSet<Fragment> _fragmentSet;

            public FragmentCreationCallbacks(IComponentCreation callback, HashSet<Fragment> fragmentSet)
            {
                _callback = callback;
                _fragmentSet = fragmentSet;
            }

            public override void OnFragmentResumed(FragmentManager fm, Fragment fragment)
            {
                base.OnFragmentResumed(fm, fragment);

                if (_fragmentSet.Contains(fragment)) return;
                _fragmentSet.Add(fragment);

                if (fragment is DialogFragment dialogFragment)
                    _callback.OnDialogCreated(dialogFragment);
                else if (fragment is BottomSheetDialogFragment bottomSheetDialogFragment)
                    _callback.OnBottomSheetDialogCreated(bottomSheetDialogFragment);
                else
                    _callback.OnFragmentCreated(fragment);
            }

            public override void OnFragmentDestroyed(FragmentManager fm, Fragment fragment)
            {
                base.OnFragmentDestroyed(fm, fragment);
                _fragmentSet.Remove(fragment);
            }
        }
    }
}