$(function () {
    $("#usernameBox").dxTextBox({
        placeholder: "Usuario",
        name: "Username",
        showClearButton: true,
        width: "100%"
    });

    $("#passwordBox").dxTextBox({
        placeholder: "Contraseña",
        name: "Password",
        mode: "password",
        showClearButton: true,
        width: "100%"
    });

    $("#submitBtnBox").dxButton({
        text: "Ingresar",
        type: "success",
        useSubmitBehavior: true,
        width: "100%"
    });
});