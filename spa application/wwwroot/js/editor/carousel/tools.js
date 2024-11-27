export function getSettings() {
  return [
    {
      name: "add",
      icon: `<img src="/js/editor/carousel/plus.svg" width="17" height="17">`,
      action: function (carousel, button) {
        carousel._addItem(null, "yehuu");
      },
    },
    {
      name: "remove",
      icon: `<img src="/js/editor/carousel/minus.svg" width="17" height="17">`,
      action: function (carousel, button) {
        let activeItem = carousel.carousel.querySelector(
          ".carousel-item.active"
        );
        let parent = activeItem.parentElement;
        let index = Array.prototype.indexOf.call(parent.children, activeItem);
        carousel._removeItem(index);
      },
    },
    {
      name: "stretched",
      icon: `<svg width="17" height="10" viewBox="0 0 17 10" xmlns="http://www.w3.org/2000/svg"><path d="M13.568 5.925H4.056l1.703 1.703a1.125 1.125 0 0 1-1.59 1.591L.962 6.014A1.069 1.069 0 0 1 .588 4.26L4.38.469a1.069 1.069 0 0 1 1.512 1.511L4.084 3.787h9.606l-1.85-1.85a1.069 1.069 0 1 1 1.512-1.51l3.792 3.791a1.069 1.069 0 0 1-.475 1.788L13.514 9.16a1.125 1.125 0 0 1-1.59-1.591l1.644-1.644z"/></svg>`,
      action: function (carousel, button) {
        carousel.toogleStretch();
        button.classList.toggle('cdx-settings-button--active');
      }
    },
  ];
}
