<template>
<div class="menubar">
    <div class="title-bar">
        <div id="deliveries" class="menu-icon">
             <img src="@/assets/images/deliveryIcon.png"/>
        </div>
        <div>
            <h4>DOSTAWY</h4>
        </div>
    </div>
    <button class="deliveries-btn">Najbliższe dostawy</button>
    <button class="deliveries-btn">Harmonogram</button>
    <button class="deliveries-btn">Wyszukaj</button>
    <button class="deliveries-btn">Dodaj</button>
    <button class="deliveries-btn">Moje dostawy</button>

    <div class="title-bar">
        <div id="orders" class="menu-icon">
             <img src="@/assets/images/orderIcon.png"/>
        </div>
        <div>
            <h4>ZAMÓWIENIA</h4>
        </div>
    </div>
    <button class="orders-btn">Przeglądaj</button>
    <button class="orders-btn">Dodaj</button>

     <div class="title-bar">
        <div id="items" class="menu-icon">
             <img src="@/assets/images/itemIcon.png"/>
        </div>
        <div>
            <h4>MATERIAŁY</h4>
        </div>
    </div>
    <button class="items-btn">Przeglądaj</button>
    <button class="items-btn">Dodaj</button>

    <div class="title-bar">
        <div id="vendors" class="menu-icon">
             <img src="@/assets/images/vendorIcon.png"/>
        </div>
        <div>
            <h4>DOSTAWCY</h4>
        </div>
    </div>
    <button class="vendors-btn">Przeglądaj</button>
    <button class="vendors-btn">Dodaj</button>

    <div class="title-bar">
        <div id="forwarders" class="menu-icon">
             <img src="@/assets/images/forwarderIcon.png"/>
        </div>
        <div>
            <h4>PRZEWOŹNICY</h4>
        </div>
    </div>
    <router-link class="forwarders-btn btn" :to="{name:'Main'}">Przeglądaj</router-link>
    <router-link class="forwarders-btn btn" :to="{name:'Main'}">Ranking</router-link>
    <router-link class="forwarders-btn btn" :to="{name:'Main'}">Dodaj</router-link>
</div>
</template>

<script>
export default {

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
    background-color: #2A9F98;
}
.menubar #orders{
    background-color: #D445EC;
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