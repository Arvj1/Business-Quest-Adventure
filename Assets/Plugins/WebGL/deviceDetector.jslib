mergeInto(LibraryManager.library, {
  DetectDeviceType: function () {
    var type = (('ontouchstart' in window) || (navigator.maxTouchPoints > 0)) ? 'Touch' : 'Keyboard';
    SendMessage('WebGLDeviceDetector', 'SetDeviceType', type);
  }
});