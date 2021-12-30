<template>
  <div class="legend">
      <div class="legend-status-ongoing">

      </div>
      <div>
          <p>standardowa dostawa</p>
      </div>
      <div class="legend-status-delayed">

      </div>
      <div>
          <p>opóźniona dostawa</p>
      </div>
      <div class="legend-status-ontime">

      </div>
      <div>
          <p>dostarczona</p>
      </div>
      <div class="legend-pallets">
          <p>16</p>
          <p>22</p>
      </div>
      <div>
          <p>- ilość palet oryginalnych</p>
          <p>- ilość palet euro</p>
      </div>
      <div class="legend-pallets legend-prio">
           <p>16</p>
          <p>22</p>
      </div>
      <div>
          <p> priorytet</p>
      </div>
  </div>
</template>

<script>
export default {

}
</script>

<style>
.legend{
    display: flex;
    justify-content: right;
    align-items: center;
    margin: 10px;
    font-size: 12px;

}
.legend-pallets {
    align-self: center;
    text-align: center;
    font-size: 12px;
    font-weight: 600;
    background-color: #5c5c5c;
    padding:  8px 5px;
    box-shadow: 2px 2px 3px rgba(20,20,20,0.7) inset;
    border-radius: 50%;
    width: 40px;
    margin: 0 10px 0 40px;
}
.legend-status-delayed{
    background-color: var(--delay);
    background-image: linear-gradient( var(--delay),#752c2c);
    height: 30px;
    width: 30px;
    margin-left:40px;
    margin-right:10px ;
}
.legend-status-ontime{
    background-color: var(--green);
    background-image: linear-gradient(var(--green),#3a5215);
    height: 30px;
    width: 30px;
    margin-left:40px;
    margin-right:10px ;
}
.legend-status-ongoing{
    height: 30px;
    width: 30px;
    margin-left:40px;
    margin-right:10px;
    background-color: var(--dark-blue);
    background-image: linear-gradient(to bottom right, var(--dark-blue),#2f3d61);
}
.legend-prio{
background-color: var(--orange);
}
</style>