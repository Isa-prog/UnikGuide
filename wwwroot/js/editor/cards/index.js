import { tools } from "./tools.js";
import { LinkButton } from "../linkButton.js";
import { ImageInput } from "../imageInput.js";

export class CardsTool {
  cards;
  static get toolbox() {
    return {
      title: "Cards",
      icon: '<img src="/js/editor/cards/icon.svg">',
    };
  }

  constructor({ data, config, api }) {
    this.data = {
      cards: data.cards || [
        { type: "text", header: "Заголовок", text: "Текст" },
      ],
    };
    this.config = config;
    this.api = api;
    this.cards = document.createElement("div");
  }

  render() {
    let cards = document.createElement("div");
    cards.classList.add("row");

    this.data.cards.forEach((card) => {
      let cardElement;
      if (card.type == "text") {
        cardElement = this._createTextCard(card.header, card.text);
      } else {
        cardElement = this._createCard(
          card.header,
          card.imageUrl,
          card.text,
          card.linkText,
          card.linkUrl
        );
      }
      cards.appendChild(cardElement);
    });

    this.cards = cards;
    return cards;
  }

  _createTextCard(header, text) {
    let card = document.createElement("div");
    card.classList.add("first-info-card");
    card.setAttribute("card-type", "text");

    let headerNode = document.createElement("h3");
    headerNode.innerHTML = header;
    headerNode.setAttribute("contenteditable", "true");

    let textNode = document.createElement("p");
    textNode.innerHTML = text;
    textNode.setAttribute("contenteditable", "true");

    let remove = document.createElement("div");
    remove.classList.add("xmark");
    remove.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="45" height="45" class="bi bi-backspace" viewBox="0 0 16 16"><path d="M15.683 3a2 2 0 0 0-2-2h-7.08a2 2 0 0 0-1.519.698L.241 7.35a1 1 0 0 0 0 1.302l4.843 5.65A2 2 0 0 0 6.603 15h7.08a2 2 0 0 0 2-2V3zM5.829 5.854a.5.5 0 1 1 .707-.708l2.147 2.147 2.146-2.147a.5.5 0 1 1 .707.708L9.39 8l2.146 2.146a.5.5 0 0 1-.707.708L8.683 8.707l-2.147 2.147a.5.5 0 0 1-.707-.708L7.976 8 5.829 5.854z"/></svg>';
    remove.onclick = () => {
      card.remove();
      if(this.cards.children.length == 0) {
        this.api.blocks.delete(this.api.blocks.getCurrentBlockIndex());
      }
    }

    card.appendChild(headerNode);
    card.appendChild(textNode);
    card.appendChild(remove);
    return card;
  }
  _createCard(header, imageUrl, text, linkText, linkUrl) {
    let card = document.createElement("div");
    card.classList.add("first-info-card");
    card.setAttribute("card-type", "full");

    let headerNode = document.createElement("h3");
    headerNode.innerHTML = header;
    headerNode.setAttribute("contenteditable", "true");
    let imageNode;
    if (imageUrl) {
      imageNode = document.createElement("img");
      imageNode.src = imageUrl;
      imageNode.alt = header;
    }

    let textNode = document.createElement("p");
    textNode.innerHTML = text;
    textNode.setAttribute("contenteditable", "true");

    let linkNode = document.createElement("a");
    linkNode.href = linkUrl;
    linkNode.classList.add("btn", "btn-outline-dark");
    linkNode.innerHTML = linkText;
    linkNode.setAttribute("contenteditable", "");

    let remove = document.createElement("div");
    remove.classList.add("xmark");
    remove.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="45" height="45" class="bi bi-backspace" viewBox="0 0 16 16"><path d="M15.683 3a2 2 0 0 0-2-2h-7.08a2 2 0 0 0-1.519.698L.241 7.35a1 1 0 0 0 0 1.302l4.843 5.65A2 2 0 0 0 6.603 15h7.08a2 2 0 0 0 2-2V3zM5.829 5.854a.5.5 0 1 1 .707-.708l2.147 2.147 2.146-2.147a.5.5 0 1 1 .707.708L9.39 8l2.146 2.146a.5.5 0 0 1-.707.708L8.683 8.707l-2.147 2.147a.5.5 0 0 1-.707-.708L7.976 8 5.829 5.854z"/></svg>';
    remove.onclick = () => {
      card.remove();
      if(this.cards.children.length == 0) {
        this.api.blocks.delete(this.api.blocks.getCurrentBlockIndex());
      }
    }

    card.appendChild(headerNode);
    if (imageUrl) {
      card.appendChild(imageNode);
    } else {
      new ImageInput(card, this.config.endpoint);
    }
    card.appendChild(textNode);
    card.appendChild(linkNode);
    card.appendChild(remove);

    let tooltip = new LinkButton(card);
    return card;
  }

  renderSettings() {
    const wrapper = document.createElement("div");

    tools.forEach((tune) => {
      let button = document.createElement("div");

      button.classList.add("cdx-settings-button");
      button.innerHTML = tune.icon;
      button.onclick = () => tune.action(this);
      wrapper.appendChild(button);
    });

    return wrapper;
  }

  save(blockContent) {
    let data = { cards: [] };
    this.cards.childNodes.forEach((card) => {
      let cardType = card.getAttribute("card-type");
      if(cardType == "full") {
        data.cards.push({
          type: "full",
          header: card.children[0].innerHTML,
          imageUrl: card.children[1].src,
          text: card.children[2].innerHTML,
          linkText: card.children[3].innerHTML,
          linkUrl: card.children[3].href,
        });
      }
      if(cardType == "text") {
        data.cards.push({
          type: "text",
          header: card.children[0].innerHTML,
          text: card.children[1].innerHTML,
        });
      }
    });
    return data;
  }
}
