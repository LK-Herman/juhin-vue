<template>
    <div class="del-header">
        <h2>Zbliżające się dostawy</h2>
        <DeliveryLegend/>
    </div>
    <div class="upcoming-container">
        <div class="item-head1">
            <h3>TYDZIEŃ {{week1}}</h3> 
            <p>
                {{moment(firstDayOfWeek1).locale("pl").format("dddd DD-MM-YYYY")}} - {{moment(lastDayOfWeek1).locale("pl").format("dddd DD-MM-YYYY")}}
            </p>
        </div>
        <div class="item-week1">
                <div>
                    <Weekly :userToken="userToken" :date="firstDayOfWeek1" />
                </div>
        </div>
        <div class="item-head2">
            <h3>TYDZIEŃ {{week2}}</h3>
            <p>
                {{moment(firstDayOfWeek2).locale("pl").format("dddd DD-MM-YYYY")}} - {{moment(lastDayOfWeek2).locale("pl").format("dddd DD-MM-YYYY")}}
            </p>
        </div>   
        <div class="item-week2">
                <div>
                    <Weekly :userToken="userToken" :date="firstDayOfWeek2" />
                </div>
        </div>
        <div class="item-head3">
            <h3>TYDZIEŃ {{week3}}</h3>
            <p>
                {{moment(firstDayOfWeek3).locale("pl").format("dddd DD-MM-YYYY")}} - {{moment(lastDayOfWeek3).locale("pl").format("dddd DD-MM-YYYY")}}
            </p>
        </div>   
        <div class="item-week3">
                <div>
                    <Weekly :userToken="userToken" :date="firstDayOfWeek3" />
                </div>
        </div>
    </div>
</template>

<script>
import Weekly from '../components/Weekly.vue'
import DeliveryLegend from '../components/DeliveryLegend.vue'
import moment from 'moment'

export default {
    props: ['userToken', 'user'],
    components: {Weekly, DeliveryLegend},
    setup(props){

        let dayOfWeek = moment().format("E") - 1
        let firstDayOfWeek1 = moment().subtract(dayOfWeek, 'days').format("YYYY-MM-DD")
        let lastDayOfWeek1 = moment(firstDayOfWeek1).add(6, 'days').format("YYYY-MM-DD")
        let firstDayOfWeek2 = moment(firstDayOfWeek1).add(7, 'days').format("YYYY-MM-DD")
        let lastDayOfWeek2 = moment(firstDayOfWeek2).add(6, 'days').format("YYYY-MM-DD")
        let firstDayOfWeek3 = moment(firstDayOfWeek2).add(7, 'days').format("YYYY-MM-DD")
        let lastDayOfWeek3 = moment(firstDayOfWeek3).add(6, 'days').format("YYYY-MM-DD")
        
        
        let week1 = moment(firstDayOfWeek1).isoWeek()
        let week2 = moment(firstDayOfWeek2).isoWeek()
        let week3 = moment(firstDayOfWeek3).isoWeek()
  
        return {week1, firstDayOfWeek1, lastDayOfWeek1, week2, firstDayOfWeek2, lastDayOfWeek2, week3, firstDayOfWeek3, lastDayOfWeek3, moment}
    }
}
</script>

<style>
.del-header{
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.upcoming-container{
    display: grid;
    
    grid-template-columns: auto auto auto;
    column-gap: 0px;
    margin: 20px 0;
    padding: 0;
    grid-template-rows: auto;
    grid-template-areas: 
    "header1 header2 header3"
    "week-one week-two week-three";
    justify-content: space-between;
    /* background-image: linear-gradient(to bottom right, #aa189e,#aa189e, #323232, #323232, #323232,#353535, #333,#333, #323232); */
}
.upcoming-container .item-week1 {
    grid-area: week-one;
    justify-self: center;
    
}
.upcoming-container .item-week2 {
    grid-area: week-two;
    justify-self: center;
    
}
.upcoming-container .item-week3 {
    grid-area: week-three;
    justify-self: center;
}
.upcoming-container .item-head1 {
    grid-area: header1;
    text-align: center;
    align-self:start;
    margin: 0;
    padding: 15px 0px;
    background-color: rgb(29, 73, 0);
    min-width: 420px;
    width: 100%;
}
.upcoming-container .item-head2 {
    grid-area: header2;
    text-align: center;
    align-self:start;
    padding: 15px 0px;
    background-color: rgb(68, 68, 68);
    min-width: 420px;
    width: 100%;
}
.upcoming-container .item-head3 {
    grid-area: header3;
    text-align: center;
    align-self:start;
    padding: 15px 0px;
    background-color: rgb(51, 51, 51);
    min-width: 420px;
    width: 100%;
}
.upcoming-container h3{
    margin-bottom: 10px;
}
</style>