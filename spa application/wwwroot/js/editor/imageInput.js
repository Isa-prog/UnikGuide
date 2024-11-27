export class ImageInput {
  constructor(container, url) {
    this.container = container;
    this.url = url;

    let label = document.createElement("label");
    label.classList.add("image-uploader");

    let input = document.createElement("input");
    input.type = "file";
    input.accept = "image/*";
    input.classList.add("image-uploader");
    label.innerHTML =
      '<svg width="20" height="20" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path d="M3.15 13.628A7.749 7.749 0 0 0 10 17.75a7.74 7.74 0 0 0 6.305-3.242l-2.387-2.127-2.765 2.244-4.389-4.496-3.614 3.5zm-.787-2.303l4.446-4.371 4.52 4.63 2.534-2.057 3.533 2.797c.23-.734.354-1.514.354-2.324a7.75 7.75 0 1 0-15.387 1.325zM10 20C4.477 20 0 15.523 0 10S4.477 0 10 0s10 4.477 10 10-4.477 10-10 10z"></path></svg>';
    label.append("Выберите изображение");
    input.onchange = async () => {
        await this.upload();
    }

    label.appendChild(input);
    container.appendChild(label);

    this.label = label;
    this.input = input;
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
        let img = document.createElement("img");
        img.classList.add("down-info");
        img.src = data.file.url;

        this.container.replaceChild(img, this.label);
        this.label.remove();
    }
  }
}
