using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Google.Android.Material.BottomSheet;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace productDemo.Feature
{
    public class Greeter : IComponentCreation
    {
        public readonly Context context;

        public Greeter(Context context)
        {
            this.context = context;
            AppExtensions.ObserveComponentCreation((Application)this.context, this);
        }

        public void OnActivityCreated(Activity activity)
        {
            if (IsExcluded(activity)) return;
            Greet($"Activity: {activity.Class.SimpleName}");
        }

        public void OnFragmentCreated(Fragment fragment)
        {
            if (IsExcluded(fragment)) return;
            Greet($"Fragment: {fragment.Class.SimpleName}");
        }

        public void OnDialogCreated(DialogFragment dialog)
        {
            if (IsExcluded(dialog)) return;
            Greet($"DialogFragment: {dialog.Class.SimpleName}");
        }

        public void OnBottomSheetDialogCreated(BottomSheetDialogFragment dialog)
        {
            if (IsExcluded(dialog)) return;
            Greet($"BottomSheetDialogFragment: {dialog.Class.SimpleName}");
        }

        public void Init()
        {
            if (!(context is Application)) return;
        }

        private void Greet(string message)
        {
            Toast.MakeText(context, message, ToastLength.Short).Show();
        }

        private bool IsExcluded(Activity activity)
        {
            var excludedActivities = new List<string>
            {
                // list of excluded activities
            };
            return excludedActivities.Contains(activity.Class.SimpleName);
        }

        private bool IsExcluded(Fragment fragment)
        {
            var excludedFragments = new List<string>
            {
                "SupportRequestManagerFragment"
                // list of other excluded fragments
            };
            return excludedFragments.Contains(fragment.Class.SimpleName);
        }

        private bool IsExcluded(DialogFragment fragment)
        {
            var excludedFragments = new List<string>
            {
                // list of excluded dialog fragments
            };
            return excludedFragments.Contains(fragment.Class.SimpleName);
        }

        private bool IsExcluded(BottomSheetDialogFragment fragment)
        {
            var excludedFragments = new List<string>
            {
                // list of excluded bottom sheet dialog fragments
            };
            return excludedFragments.Contains(fragment.Class.SimpleName);
        }
    }
}