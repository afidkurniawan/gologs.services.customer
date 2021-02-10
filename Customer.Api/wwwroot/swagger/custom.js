(function () {
  window.addEventListener("load", function () {
    setTimeout(function () {
      const logo = document.getElementsByClassName('link');
      logo[0].href = "https://go-logs.com";
      logo[0].target = "_blank";
      logo[0].children[0].alt = "Go-Logs";
      logo[0].children[0].src = "/swagger/resources/go-logs_white_168x40.png";

      const linkIcon32 = document.createElement('link');
      linkIcon32.type = 'image/png';
      linkIcon32.rel = 'icon';
      linkIcon32.href = '/swagger/resources/favicon-32x32.png';
      linkIcon32.sizes = '32x32';
      document.getElementsByTagName('head')[0].appendChild(linkIcon32);

      const linkIcon16 = document.createElement('link');
      linkIcon16.type = 'image/png';
      linkIcon16.rel = 'icon';
      linkIcon16.href = '/swagger/resources/favicon-16x16.png';
      linkIcon16.sizes = '16x16';
      document.getElementsByTagName('head')[0].appendChild(linkIcon16);
    });
  });
})();
