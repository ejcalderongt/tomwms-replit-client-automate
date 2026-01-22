$(function () {
    const usernameInput = $("<input>").attr("type", "hidden").attr("name", "username").appendTo("#loginForm");
    const passwordInput = $("<input>").attr("type", "hidden").attr("name", "password").appendTo("#loginForm");

    $("#usernameBox").dxTextBox({
        placeholder: "Usuario",
        name: "Username",
        showClearButton: true,
        width: "100%",
        onValueChanged: function (e) {
            usernameInput.val(e.value);
        }
    });

    $("#passwordBox").dxTextBox({
        placeholder: "Contraseña",
        name: "Password",
        mode: "password",
        showClearButton: true,
        width: "100%",
        onValueChanged: function (e) {
            passwordInput.val(e.value);
        }
    });

    $("#submitBtnBox").dxButton({
        text: "Ingresar",
        type: "success",
        useSubmitBehavior: true,
        width: "100%"
    });
});