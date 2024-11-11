function openPopup(popupId) {
    document.getElementById(popupId).classList.add('active');
  }

  function closePopup(popupId) {
    document.getElementById(popupId).classList.remove('active');
  }

  function switchPopup(currentPopupId, targetPopupId) {
    closePopup(currentPopupId);
    openPopup(targetPopupId);
  }