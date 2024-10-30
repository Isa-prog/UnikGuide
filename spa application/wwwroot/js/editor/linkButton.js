export class LinkButton {
  _button;
  _wraper;
  _urlLinkInput;
  onRemove = function () {};
  onEdit = function () {};
  static onFocus = [function (linkTooltip) {}];

  constructor(linkContainer, supportRemove = false) {
    this._button = linkContainer.querySelector("a.btn");

    let wraper = document.createElement("div");
    wraper.classList.add("link-button-wrapper");
    wraper.classList.add("link-button-wrapper--toggle");

    if (supportRemove) {
      let remove = document.createElement("input");
      remove.type = "button";
      remove.classList.add("cdx-input");
      remove.value = "Удалить";
      remove.onclick = () => {
        this.onRemove();
        this._button.remove();
        this._wraper.remove();
      };
      wraper.appendChild(remove);
    }

    let urlLink = document.createElement("div");
    urlLink.setAttribute("contenteditable", "");
    urlLink.setAttribute("data-placeholder", "Введите ссылку");
    urlLink.classList.add("cdx-input");
    wraper.appendChild(urlLink);

    this._urlLinkInput = urlLink;
    this._wraper = wraper;

    linkContainer.appendChild(wraper);

    this._button.onclick = () => {
      LinkButton.onFocus.map((func) => func(this));
    };

    let button = this._button;
    let onEdit = this.onEdit;
    urlLink.addEventListener(
      "input",
      function () {
        button.href = urlLink.innerHTML;
        onEdit();
      },
      false
    );
    let self = this;
    LinkButton.onFocus.push(function (linkTooltip) {
      if (linkTooltip === self) {
        self.toggle();
      } else {
        wraper.classList.add("link-button-wrapper--toggle");
      }
    });
    console.log(LinkButton.onFocus);
  }

  toggle() {
    this._wraper.classList.toggle("link-button-wrapper--toggle");
    this._urlLinkInput.innerHTML = this._button.getAttribute("href");
  }
}
