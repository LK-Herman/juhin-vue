<template>
<div class="menubar">
    <div class="title-bar">
        <div id="deliveries" class="menu-icon">
             <img src="@/assets/images/deliveryIcon.png"/>
        </div>
        <div>
            <h4 >DOSTAWY</h4>
        </div>
    </div>
    <router-link :to="{name:'Upcoming'}" class="deliveries-btn btn">Najbliższe dostawy</router-link>
    <router-link :to="{name:'Deliveries', params: {userToken: userToken}}" class="deliveries-btn btn">Przeglądaj</router-link>
    <router-link :to="{name:'DeliverySchedule'}" class="deliveries-btn btn">Grafik dostaw</router-link>
    <router-link :to="{name:'DeliverySearch'}" class="deliveries-btn btn">Wyszukaj</router-link>
    <!-- <router-link  class="deliveries-btn btn">Dodaj</router-link> -->
    <router-link :to="{name:'DeliveryUser'}" class="deliveries-btn btn">Moje dostawy</router-link>

    <div class="title-bar">
        <div id="orders" class="menu-icon">
             <img src="@/assets/images/orderIcon.png"/>
        </div>
        <div>
            <h4>ZAMÓWIENIA</h4>
        </div>
    </div>
    <router-link :to="{name:'Orders', params: {userToken: userToken}}" class="orders-btn btn">Przeglądaj</router-link>
    <router-link :to="{name:'OrderAdd'}" class="orders-btn btn">Dodaj</router-link>

     <div class="title-bar">
        <div id="items" class="menu-icon">
             <img src="@/assets/images/itemIcon.png"/>
        </div>
        <div>
            <h4>TOWARY</h4>
        </div>
    </div>
    <router-link :to="{name:'Items'}" class="items-btn btn">Dodaj</router-link>
    <router-link :to="{name:'ItemAdd'}" class="items-btn btn">Dodaj</router-link>

    <div class="title-bar">
        <div id="vendors" class="menu-icon">
             <img src="@/assets/images/vendorIcon.png"/>
        </div>
        <div>
            <h4>DOSTAWCY</h4>
        </div>
    </div>
    <router-link class="vendors-btn btn" :to="{name:'Vendors', params: {userToken: userToken}}">Przeglądaj</router-link>
    <router-link class="vendors-btn btn" :to="{name:'VendorAdd', params: {userToken: userToken}}">Dodaj</router-link>

    <div class="title-bar">
        <div id="forwarders" class="menu-icon">
             <img src="@/assets/images/forwarderIcon.png"/>
        </div>
        <div>
            <h4>PRZEWOŹNICY</h4>
        </div>
    </div>
    <router-link class="forwarders-btn btn" :to="{name:'Forwarders', params: {userToken: userToken}}">Przeglądaj</router-link>
    <router-link class="forwarders-btn btn" :to="{name:'ForwarderAdd'}">Dodaj</router-link>
    <router-link class="forwarders-btn btn" :to="{name:'ForwarderRanking'}">Ranking</router-link>
</div>
</template>

<script>

export default {
    props: ['userToken', 'user'],
setup(props){
   
    return{ }
}
}
</script>

<style>
.menubar{
    display: flex;
    flex-flow: column;
    background-color:var(--back-dark);
    padding: 3px 0 0 0;
    margin: 0;
    height: 100%;
    box-shadow: 4px 0px 4px rgba(30,30,30,0.2);
}
.menubar h4 {
    font-weight: 300;
}
.menubar .menu-icon{
    display: inline-block;
    padding: 8px 0;    
    text-align: center;
    width: 60px;
    margin: 3px auto;
}
.menubar #deliveries {
    background-color: var(--deliveries);
}
.menubar #orders{
    background-color: var(--orders);
}
.menubar #items{
    background-color: #218FF3;
}
.menubar #vendors{
    background-color: #528322;
}
.menubar #forwarders{
    background-color: #613FC0;
}
.menubar .deliveries-btn:hover{
    background-color: #2A9F98;
}
.menubar .orders-btn:hover{
    background-color: #D445EC;
}
.menubar .items-btn:hover{
    background-color: #218FF3;
}
.menubar .vendors-btn:hover{
    background-color: #528322;
}
.menubar .forwarders-btn:hover{
    background-color: #613FC0;
}

.menubar .title-bar{
    display: grid;
    grid-template-columns: 60px 1fr;
    column-gap: 10px;
} 
.menubar .title-bar div{
    align-self: center;
}
.menubar button, .menubar .btn{
    margin: 3px 0;
    box-shadow: 6px 4px 8px rgba(30,30,30,0.5);
}

</style>