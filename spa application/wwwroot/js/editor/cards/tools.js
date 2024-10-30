export const tools = [
  {
    name: "addTextCard",
    title: "Добавить текстовую карточку",
    icon: `<img src="/js/editor/cards/card-heading.svg" width="17" height="17">`,
    action: function (cardsTool) {
      cardsTool.cards.appendChild(
        cardsTool._createTextCard("Заголовок", "Текст")
      );
    },
  },
  {
    name: "addCard",
    title: "Добавить карточку",
    icon: `<img src="/js/editor/cards/icon.svg" width="17" height="17">`,
    action: function (cardsTool) {
      cardsTool.cards.appendChild(
        cardsTool._createCard(
          "Заголовок",
          undefined,
          "Текст",
          "Узнать больше",
          "https://google.com"
        )
      );
    },
  },
];
