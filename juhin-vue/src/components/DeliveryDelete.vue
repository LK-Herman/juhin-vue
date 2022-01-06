<template>
    <div id="myModal" class="modal">
        <div class="modal-content">

                <div class="modal-question">
                    <p>Czy na pewno chcesz usunąć?</p>
                </div>
                <div class="modal-buttons" >
                    <button id="yes" @click="handleYes">Tak</button>
                    <button id="no" @click="handleNo">Nie</button>
                </div>
            
        </div>

    </div>

</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import {useRouter} from 'vue-router'
import deleteDelivery from '../composables/deleteDelivery.js'

export default {
    props:['userToken','id','orders'],
    setup(props){
        const mainUrl = urlHolder
        const router = useRouter()
        const {delDel, error} = deleteDelivery(mainUrl, props.userToken)

        const handleYes = async () =>{
            await delDel(props.id,props.orders)
            .finally(
                 router.back
                )
            
        }
        const handleNo = () =>{
             var modal = document.getElementById("myModal");
             modal.style.display = "none"
        }

        return {handleYes, handleNo, error}
    }
};
</script>

<style>
/* The Modal (background) */
.modal {
  display: none; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 99; /* Sit on top */
  padding-top: 300px; /* Location of the box */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0,0,0); /* Fallback color */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
  position: relative;
  background-color: #313131;
  color: #fff;
  border-radius: 20px;
  margin: auto;
  padding: 20px;
  border: 4px solid rgb(144, 144, 144);
  width: 460px;
  box-shadow: 8px 8px 12px 0 rgba(0, 0, 0, 0.5),-8px -8px 12px 0 rgba(0,0,0,0.5);
  -webkit-animation-name: animatetop;
  -webkit-animation-duration: 0.4s;
  animation-name: animatetop;
  animation-duration: 0.4s
}
.modal-content .modal-question {
    text-align: center;
    padding: 20px 20px 40px 20px;
}
.modal-content .modal-buttons {
    display: flex;
    justify-content: space-between;
    padding: 20px 40px;
}
.modal-content .modal-buttons button{
    min-width: 160px;
    
}
.modal-content .modal-buttons button#yes{
    background-color: rgb(197, 0, 0);
}
.modal-content .modal-buttons button#yes:hover{
    background-color: rgb(253, 0, 0);
}
.modal-content .modal-buttons button#no{
    background-color: rgb(0, 100, 38);
}
.modal-content .modal-buttons button#no:hover{
    background-color: rgb(0, 153, 59);
}

/* Add Animation */
@-webkit-keyframes animatetop {
  from {top:-300px; opacity:0} 
  to {top:0; opacity:1}
}

@keyframes animatetop {
  from {top:-300px; opacity:0}
  to {top:0; opacity:1}
}

/* The Close Button */
.close {
  color: white;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
}

.modal-header {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}

.modal-body {padding: 2px 16px;}

.modal-footer {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}
</style>