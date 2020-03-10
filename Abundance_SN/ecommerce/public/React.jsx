

import ProductSlider from './productslider';



export const HomeView = () => 
<div>

  <div className="container-fluid mainProducts">
    <ProductSlider />
  </div>

</div>;
ReactDOM.render(
  <ProductSlider />,
  document.getElementById('root')
);



