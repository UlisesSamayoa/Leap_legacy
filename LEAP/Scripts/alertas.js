function _ErrorAlert(name) {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Error " + name
    });
}
function _SuccessAlert(name) {
Swal.fire({
    position: "center",
    icon: "success",
    title: "Done,"+ name,
    showConfirmButton: false,
    timer: 1500
});
}