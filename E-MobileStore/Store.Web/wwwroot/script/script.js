$(document).ready(function () {
    var editor = CKEDITOR.instances['txtDescription'];
    if (editor) {
        editor.destroy(true);
    }
    CKEDITOR.replace('txtDescription', {
        enterMode: CKEDITOR.ENTER_BR,
        filebrowserUploadUrl: '/Admin/ImageUpload/UploadImage',
        filebrowserBrowseUrl: '/Admin/ImageUpload/FileBrowserCKEDITOR',

    })
})