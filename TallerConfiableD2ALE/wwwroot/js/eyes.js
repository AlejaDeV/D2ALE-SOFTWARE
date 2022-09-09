const iconEye = document.querySelector(".iconEye");

iconEye.addEventListener('click',function(){
    const icon = this.querySelector('i');//Accedemos al iconEye y buscamos el elemento de etiqueta i
    if(this.previousElementSibling.type ==='password'){//Validamos si el elemento(hermano=sibling) anterior es de tipo password (previous)
        this.previousElementSibling.type = 'text';
        icon.classList.remove("bi-eye-fill");
        icon.classList.add("bi-eye-slash-fill");
        
    }else{
        this.previousElementSibling.type = 'password';
        icon.classList.remove("bi-eye-slash-fill");
        icon.classList.add("bi-eye-fill");
        
    }
})