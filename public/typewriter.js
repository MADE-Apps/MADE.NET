(function () {
  // Typewriter animation for package names
  const packages = [
    "MADE.Collections",
    "MADE.Data.Converters",
    "MADE.Data.EFCore",
    "MADE.Data.Serialization",
    "MADE.Data.Validation",
    "MADE.Diagnostics",
    "MADE.Networking",
    "MADE.Runtime",
    "MADE.Testing",
    "MADE.Threading",
    "MADE.Web",
    "MADE.Web.Mvc",
  ];

  let current = 0;

  function typewriter() {
    var target = document.getElementById("typewriter-target");
    if (!target) return;

    var text = target.textContent;
    var next = packages[(current + 1) % packages.length];

    eraseAndType(target, text, next, function () {
      current = (current + 1) % packages.length;
      setTimeout(typewriter, 3500);
    });
  }

  function eraseAndType(el, oldText, newText, done) {
    var i = oldText.length;

    function erase() {
      if (i > 0) {
        i--;
        el.textContent = oldText.substring(0, i);
        setTimeout(erase, 40);
      } else {
        typeText(el, newText, 0, done);
      }
    }

    erase();
  }

  function typeText(el, text, i, done) {
    if (i <= text.length) {
      el.textContent = text.substring(0, i);
      setTimeout(function () {
        typeText(el, text, i + 1, done);
      }, 60);
    } else {
      done();
    }
  }

  function init() {
    setTimeout(typewriter, 3500);
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", init);
  } else {
    init();
  }
})();
