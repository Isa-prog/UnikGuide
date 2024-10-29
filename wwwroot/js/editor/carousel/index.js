import { getSettings } from "./tools.js";
import { InputSlideManager } from "./inputSlideManager.js";

export class CarouselTool {
  id;
  carousel;
  api;
  FileUploader;
  constructor({ data, api, config }) {
    this.data = {
      items: data.items || [
        {
          caption: "asdasdasdd",
          active: true,
        },
      ],
      stretched: data.stretched || false,
    };
    this.api = api;
    this.config = config;
  }
  static get toolbox() {
    return {
      title: "Carousel",
      icon: '<img src="/js/editor/carousel/icon.svg">',
    };
  }
  render() {
    this.id = "id" + this._uuidv4();
    let carousel = document.createElement("div");
    carousel.classList.add("carousel");
    carousel.classList.add("slide");
    carousel.setAttribute("data-bs-ride", "carousel");
    carousel.id = this.id;

    let carouselIndicators = document.createElement("div");
    carouselIndicators.classList.add("carousel-indicators");
    carousel.appendChild(carouselIndicators);

    let carouselInner = document.createElement("div");
    carouselInner.classList.add("carousel-inner");
    carousel.appendChild(carouselInner);

    this.data.items.forEach((item, index) => {
      let indicator = this._createIndicator(index, item.active);
      carouselIndicators.appendChild(indicator);

      let itemElement = this._createItem(item.active, item.url, item.caption);
      carouselInner.appendChild(itemElement);
    });

    carousel.appendChild(this._createControl("prev"));
    carousel.appendChild(this._createControl("next"));

    this.carousel = carousel;
    return this.carousel;
  }
  _createIndicator(slide, active = false) {
    let indicator = document.createElement("button");
    indicator.type = "button";
    indicator.setAttribute("data-bs-target", "#" + this.id);
    indicator.setAttribute("data-bs-slide-to", slide);
    if (active) {
      indicator.classList.add("active");
    }
    return indicator;
  }
  _createItem(active = false, url, text) {
    let image = this._createImg(url);

    let item = document.createElement("div");
    item.classList.add("carousel-item");
    item.appendChild(image);

    let caption = this._createCaption();
    let labelContainer = document.createElement("div");
    labelContainer.classList.add("image-uploader-container");
    let imageInput = document.createElement("input");
    imageInput.type = "file";
    imageInput.accept = "image/*";
    imageInput.classList.add("image-uploader");

    let label = document.createElement("label");
    label.classList.add("image-uploader");
    label.innerHTML =
      '<svg width="20" height="20" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path d="M3.15 13.628A7.749 7.749 0 0 0 10 17.75a7.74 7.74 0 0 0 6.305-3.242l-2.387-2.127-2.765 2.244-4.389-4.496-3.614 3.5zm-.787-2.303l4.446-4.371 4.52 4.63 2.534-2.057 3.533 2.797c.23-.734.354-1.514.354-2.324a7.75 7.75 0 1 0-15.387 1.325zM10 20C4.477 20 0 15.523 0 10S4.477 0 10 0s10 4.477 10 10-4.477 10-10 10z"></path></svg>';
    label.append("Выберите изображение");
    label.appendChild(imageInput);

    labelContainer.appendChild(label);

    this.FileUploader = new InputSlideManager(
      image,
      labelContainer,
      caption,
      this.config.endpoint,
    );
    item.appendChild(labelContainer);
    item.appendChild(caption);

    if (active) {
      item.classList.add("active");
    }
    return item;
  }
  _createImg(url) {
    let image = document.createElement("img");
    if (url) image.src = url;
    image.classList.add("d-block");
    image.setAttribute("alt", "...");
    return image;
  }
  _createControl(direction) {
    let control = document.createElement("button");
    control.type = "button";
    control.classList.add("carousel-control-" + direction);
    control.setAttribute("data-bs-slide", direction);
    control.setAttribute("data-bs-target", "#" + this.id);

    let arrow = document.createElement("span");
    arrow.classList.add("carousel-control-" + direction + "-icon");
    control.appendChild(arrow);

    return control;
  }
  _createCaption() {
    let caption = document.createElement("div");
    caption.classList.add("carousel-caption");

    let header = document.createElement("h5");
    header.setAttribute("contentEditable", "true");
    header.innerHTML = "Заголовок";

    let text = document.createElement("p");
    text.setAttribute("contentEditable", "true");
    text.innerHTML = "Текст";

    caption.appendChild(header);
    caption.appendChild(text);

    return caption;
  }
  _addItem(url, text) {
    let item = this._createItem(false, url, text);
    this.carousel.querySelector(".carousel-inner").appendChild(item);

    let indicator = this._createIndicator(this.data.items.length, false);
    this.carousel.querySelector(".carousel-indicators").appendChild(indicator);

    this.data.items.push({ url: url, caption: text, active: false });
  }
  _removeItem(index) {
    if (this.data.items.length > 1) {
      console.log(index);
      this.carousel
        .querySelector(".carousel-inner")
        .removeChild(
          this.carousel.querySelector(".carousel-inner").childNodes[index]
        );
      this.carousel
        .querySelector(".carousel-indicators")
        .removeChild(
          this.carousel.querySelector(".carousel-indicators").childNodes[index]
        );
      this.carousel
        .querySelector(".carousel-inner")
        .childNodes[0].classList.add("active");
      this.carousel
        .querySelector(".carousel-indicators")
        .childNodes[0].classList.add("active");
      console.log(this.carousel.querySelector(".carousel-inner").innerHTML);
      this.data.items.splice(index, 1);
      this._refreshSlideTo();
    } else {
      console.log("asdasd");
      this.api.blocks.delete(this.api.blocks.getCurrentBlockIndex());
    }
  }
  _refreshSlideTo() {
    this.carousel
      .querySelector(".carousel-indicators")
      .childNodes.forEach((indicator, index) => {
        indicator.setAttribute("data-bs-slide-to", index);
      });
  }
  _uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
      (
        c ^
        (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
      ).toString(16)
    );
  }
  toogleStretch() {
    this.data.stretched = !this.data.stretched;
    this.api.blocks.stretchBlock(
      this.api.blocks.getCurrentBlockIndex(),
      !!this.data.stretched
    );
  }
  renderSettings() {
    const settings = getSettings();
    const wrapper = document.createElement("div");

    settings.forEach((tune) => {
      let button = document.createElement("div");

      button.classList.add("cdx-settings-button");
      button.innerHTML = tune.icon;
      button.onclick = () => tune.action(this, button);
      wrapper.appendChild(button);
    });

    return wrapper;
  }
  save(blockContent) {
    let data = { items: [], stretched: this.data.stretched };
    blockContent.querySelector(".carousel-inner").childNodes.forEach((item) => {
      let mysrc = "";
      if (item.querySelector("img").hasAttribute("src")) {
        mysrc = item.querySelector("img").getAttribute("src");
      }
      let button = { url: "", text: "" };
      if(item.querySelector(".carousel-caption a"))
      {
        button = {
          url: item.querySelector(".carousel-caption a").getAttribute("href"),
          text: item.querySelector(".carousel-caption a").innerHTML,
        }
      }
      data.items.push({
        url: mysrc,
        caption: {
          header: item.querySelector(".carousel-caption h5").innerHTML,
          text: item.querySelector(".carousel-caption p").innerHTML,
          button: button,
        },
        active: item.classList.contains("active"),
      });
    });
    return data;
  }
}
