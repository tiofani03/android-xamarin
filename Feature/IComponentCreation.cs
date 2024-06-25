using Android.App;
using Google.Android.Material.BottomSheet;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace productDemo.Feature
{
    public interface IComponentCreation
    {
        public void OnActivityCreated(Activity activity);
        
        public void OnFragmentCreated(Fragment fragment);
        
        public void OnDialogCreated(DialogFragment dialog);
        
        public void OnBottomSheetDialogCreated(BottomSheetDialogFragment dialog);
    }
}