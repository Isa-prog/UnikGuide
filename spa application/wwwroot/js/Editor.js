import { CarouselTool } from "./editor/carousel/index.js";
import { CardsTool } from "./editor/cards/index.js";

(function () {
  window.MyEditor = {
    init: function (id) {
      const editor = new EditorJS({
        holder: id,
        tools: {
          image: {
            class: window.ImageTool,
            config: {
              endpoints: {
                byFile: "/editor/imageUpload",
                byUrl : "/editor/fetchUrl",
              },
            },
          },
          header: {
            class: Header,
            tunes: ["alignment"],
          },
          embded: {
            class: Embed,
            inlineToolbar: true,
            config: {
              services: {
                youtube: true,
                coub: true,
                instagram: true,
                twitter: true,
                imgur: true,
                pinterest: true,
                facebook: true,
              },
            },
          },
          carousel: {
            class: CarouselTool,
            inlineToolbar: true,
            config: {
              endpoint: "/editor/imageUpload",
            },
          },
          cards: {
            class: CardsTool,
            inlineToolbar: true,
            tunes: ["alignment"],
            config: {
              endpoint: "/editor/imageUpload",
            },
          },
          paragraph: {
            class: Paragraph,
            inlineToolbar: true,
            tunes: ["alignment"],
            config: {
              placeholder: "Введите текст",
            },
          },
          alignment: {
            class: AlignmentBlockTune,
            config: {
              default: "left",
            },
          },
        },
      });
      console.log("work " + id);
      window.editor = editor;
    },
  };
})();
window.MyEditor.init("editorjs");

document.getElementById("savepost").onclick = function () {
  window.editor.save().then((outputData) => {
    console.log(JSON.stringify(outputData));
  });
};
