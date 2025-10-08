using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.ProfileDialog;

namespace Client.Helper.Implementations.ProfileModal
{
    public class ProfileModal
    {
        private readonly IModalService _modal;
        private readonly ModalOptions _option;

        public ProfileModal(IModalService modal)
        {
            var option = new ModalOptions
            {
                UseCustomLayout = true
            };

            _option = option;
            _modal = modal;
        }

        public void Show()
        {
            var parameter = new ModalParameters();
            _modal.Show<ProfileDialog>(parameter, _option);
        }
    }
}
