import React, { useState } from "react";

let pizzas = [{
   id: 1, name: 'Cheese pizza', description: 'very cheesy'
 },
 {
   id: 2, name: 'Al Tono pizza', description: 'lots of tuna'
}];

const Pizza = ({ pizza }) => {
   const [data, setData] = useState(pizza);
   const [dirty, setDirty] = useState(false);

   function update(value, fieldName, obj) {
     setData({ ...obj, [fieldName] : value });
     setDirty(true);
   }

   function onSave() {
     setDirty(false);
     // make rest call
   }

   return (<React.Fragment>
     <div>
     <h3>
       <input onChange={(evt) => update(evt.target.value, 'name', data)} value={data.name} /> 
     </h3>
     <div>
       <input onChange={(evt) => update(evt.target.value, 'description', data)} value={data.description} />
     </div>
     {dirty ? 
      <div><button onClick={onSave}>Save</button></div> : null
     }
     </div>
   </React.Fragment>);
}

const Main = () => {
  const data = pizzas.map(pizza => <Pizza pizza={pizza} />)

  return (<React.Fragment>
    {data}
  </React.Fragment>)
  }

export default Main;