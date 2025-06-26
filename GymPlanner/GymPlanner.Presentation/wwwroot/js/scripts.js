window.bootstrapInterop = {
  showModal: (modalId) => {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
      var modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  },
  hideModal: (modalId) => {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
      var modal = bootstrap.Modal.getInstance(modalElement);
      if (modal) {
        modal.hide();
      }
    }
  },
};
