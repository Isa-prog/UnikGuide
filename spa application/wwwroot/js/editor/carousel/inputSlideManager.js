import { LinkButton } from "../linkButton.js";

export class InputSlideManager {
  constructor(image, container, caption, url) {
    this.image = image;
    this.container = container;
    this.caption = caption;
    console.log(caption);
    if (!image.getAttribute("src")) {
      this.label = container.querySelector("label");
      this.input = this.label.querySelector("input");
      this.input.click();

      this.input.onchange = async () => {
        await this.upload();
      };
    }
    this.url = url;
  }

  async upload() {
    let formData = new FormData();
    formData.append("image", this.input.files[0]);
    let response = await fetch(this.url, {
      method: "POST",
      body: formData,
    });
    let data = await response.json();
    if (data.success) {
      this.image.src = data.file.url;
      this.change();
    }
  }

  change() {
    this.label.remove();
    this.input.remove();

    let input = document.createElement("input");
    this.input = input;

    input.type = "button";
    input.classList.add("image-uploader");
    input.value = "Добавить кнопку-ссылку";
    
    input.onclick = () => {
      this.caption.appendChild(this._createATag());
      input.remove();
      let linkTooltip = new LinkButton(this.caption, true);
      linkTooltip.onRemove = () => {
        this.change();
      };
    };

    this.container.appendChild(this.input);
  }

  _createATag() {
    let container = document.createElement("div");
    container.classList.add("side-btn");
    let a = document.createElement("a");
    a.classList.add("btn", "btn-dark");
    a.href = "#";
    a.innerText = "Узнать больше";
    a.setAttribute("contentEditable", "");
    container.appendChild(a);
    return container;
  }
}
