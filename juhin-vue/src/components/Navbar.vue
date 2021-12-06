<template>
<div class="navbar">
    <div class="logo">
        JUHIN
    </div>
    <div class="logobar-out">
        <div class="logobar-in">
            <div>
                <p id="sublogo"> WAREHOUSE MANAGEMENT </p>
            </div>
            <div class="logobar-links">
                <p v-if="isLogged" class="links-item">{{email}}</p>
                <p class="links-item">Zarejestruj się</p>
                <p v-if="isLogged" class="links-item">Wyloguj</p>
                <router-link v-else :to="{name:'Login'}" class="links-item">Zaloguj się</router-link>
            </div>
        </div>
    </div>
        
</div>
</template>

<script>

export default {
    props:['isLogged', 'email'],
    setup(props, context){
        

        return {  }
    }

}
</script>

<style>

.navbar{
    display: grid;
    grid-template-rows: 60px 60px;
    column-gap: 0;
    
}
.navbar .logo
{
    font-family: 'Amaranth', sans-serif;
    font-size: 61px;
    z-index: 10;
    padding: 0px 0 0 15px;
}
.navbar .logobar-out{
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: #536BAA;
     
}
.navbar .logobar-in{
    display: flex;
    align-items: center;
    margin: 0px auto;
    width: 100%;
    padding: 10px 0px;
    background-color: #6D8CDC;
    margin: auto;
    box-shadow: 0px 2px 4px rgba(50, 50, 50, 0.4);
}
.navbar .logobar-in .logobar-links{
    margin-left: auto;
    display: flex;
    align-items: center;
    padding-right: 36px;
}
.navbar .logobar-in .logobar-links .links-item{
    padding-left: 36px;
    font-size: 14px;
    font-weight: 500;
}
#sublogo{
    font-family: 'Amaranth', sans-serif;
    font-size: 16px;
    padding-left: 36px;
}
</style>