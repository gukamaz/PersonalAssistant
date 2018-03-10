(function () {
    var letters = 'abgdevzTiklmnopJrstufqRySCcZwWxjh';
    var addend = 4304;
    var switchChar = '~';
    var inputs = [];
    var inputTexts = document.querySelectorAll('input[data-geokb]');
    var inputAreas = document.querySelectorAll('textarea[data-geokb]');
    for (var i = 0; i < inputTexts.length; i++) {
        inputs.push(inputTexts[i]);
    }
    for (var i = 0; i < inputAreas.length; i++) {
        inputs.push(inputAreas[i]);
    }

    for (var i = 0; i < inputs.length; i++) {
        var elem = inputs[i];
        var parent = elem.parentElement;

        var switchButton = document.createElement('span');
        switchButton.className = 'geo-flag';
        switchButton.style.verticalAlign = 'middle';
        switchButton.style.width = '30px';
        switchButton.style.height = '30px';
        switchButton.style.display = 'inline-block';
        switchButton.style.background = "url('http://events.eventact.com/Themes/flags/Georgia.png') no-repeat";
        switchButton.style.opacity = elem.getAttribute('data-geokb') === 'true' ? '1' : '0.5';

        var isSwitchDisabled = elem.getAttribute('data-switch-disabled') === 'true';
        if (!isSwitchDisabled) {
            parent.insertBefore(switchButton, elem);
            switchButton.addEventListener('click', function () {
                var isEnabled = this.nextSibling.getAttribute('data-geokb') === 'true';
                this.nextSibling.setAttribute('data-geokb', isEnabled ? 'false' : 'true');
                isEnabled = !isEnabled;
                this.style.opacity = isEnabled ? '1' : '0.5';
            });
        }
        elem.addEventListener('keypress', function (e) {
            var isEnabled = this.getAttribute('data-geokb') === 'true';
            var keyCode = e.keyCode || e.which;
            if (!isSwitchDisabled && keyCode === switchChar.charCodeAt(0)) {
                this.previousSibling.click();
                e.preventDefault();
            }
            if (isEnabled) {
                var charIndex = letters.indexOf(String.fromCharCode(keyCode));
                if (charIndex > -1) {
                    var newIndex = this.selectionStart + 1;
                    var leftText = this.value.substring(0, this.selectionStart);
                    var rightText = this.value.substring(this.selectionEnd);
                    this.value = leftText + String.fromCharCode(charIndex + addend) + rightText;
                    this.setSelectionRange(newIndex, newIndex);
                    e.preventDefault();
                }
            }
        });
    }
})();