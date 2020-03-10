// import React, { Component } from 'react';
// // import { Link } from 'react-router';
// // import logo from './logo.svg';
// // import './App.css';

// import {
//     Carousel,
//     CarouselItem,
//     CarouselControl,
//     CarouselIndicators,
//     CarouselCaption
// } from 'reactstrap';

// import './carousel.css';





// const items = [
//     {
//         src: 'https://images-na.ssl-images-amazon.com/images/G/01/AMAZON_FASHION/2017/EDITORIAL/WINTER_2/GATEWAY/DESKTOP/HERO_W_S_Pumps_1x._CB490669730_.jpg',
//         altText: 'Slide 1',
//         caption: 'Slide 1'
//     },
//     {
//         src: 'https://www.konga.com/media/upload/slideshow/items/2018-02-05-03-02-301020400712.png',
//         altText: 'Slide 2',
//         caption: 'Slide 2'
//     },
//     {
//         src: 'https://www.konga.com/media/upload/slideshow/items/2018-02-12-10-02-19584145862.png',
//         altText: 'Slide 3',
//         caption: 'Slide 3'
//     }
// ];

// class Carouselroll extends Component {
//     constructor(props) {
//         super(props);

//         this.toggle = this.toggle.bind(this);


//         this.state = { activeIndex: 0 };
//         this.next = this.next.bind(this);
//         this.previous = this.previous.bind(this);
//         this.goToIndex = this.goToIndex.bind(this);
//         this.onExiting = this.onExiting.bind(this);
//         this.onExited = this.onExited.bind(this);
//     }

//     toggle() {
//         this.setState({ isOpen: !this.state.isOpen });
//     }


//     onExiting() {
//         this.animating = true;
//     }

//     onExited() {
//         this.animating = false;
//     }

//     next() {
//         if (this.animating) return;
//         const nextIndex = this.state.activeIndex === items.length - 1 ? 0 : this.state.activeIndex + 1;
//         this.setState({ activeIndex: nextIndex });
//     }

//     previous() {
//         if (this.animating) return;
//         const nextIndex = this.state.activeIndex === 0 ? items.length - 1 : this.state.activeIndex - 1;
//         this.setState({ activeIndex: nextIndex });
//     }

//     goToIndex(newIndex) {
//         if (this.animating) return;
//         this.setState({ activeIndex: newIndex });
//     }



//     render() {

//         const { activeIndex } = this.state;

//         const slides = items.map((item) => {
//             return (
//                 <CarouselItem
//                     onExiting={this.onExiting} onExited={this.onExited} key={item.src} >
//                     <img src={item.src} alt={item.altText} />
//                     <CarouselCaption captionText={item.caption} captionHeader={item.caption} />
//                 </CarouselItem>
//             );
//         });


//         return (
//             <div>


//                 <div className="container-fluid mainBanner p-0">
//                     <Carousel className="container-fluid p-0"
//                         activeIndex={activeIndex}
//                         next={this.next}
//                         previous={this.previous}
//                     >
//                         <CarouselIndicators items={items} activeIndex={activeIndex} onClickHandler={this.goToIndex} />
//                         {slides}
//                         <CarouselControl direction="prev" directionText="Previous" onClickHandler={this.previous} />
//                         <CarouselControl direction="next" directionText="Next" onClickHandler={this.next} />
//                     </Carousel>
//                 </div>



//                 <div className="container-fluid bg-secondary">
//                     <div className="row">
//                         <div className="col-md-4 col-sm-12 h100 bgUrl1"></div>
//                         <div className="col-md-4 col-sm-12 h100 bgUrl2"></div>
//                         <div className="col-md-4 col-sm-12 h100 bgUrl3"></div>
//                     </div>
//                 </div>
//             </div>
//         );
//     }
// }
// export default Carouselroll;



import React, { Component } from 'react';
import { TestComponent } from './components/TestComponent.jsx'
import {CarouselControl,CarouselIndicators} from 'reactstrap/lib';
import Carousel from 'reactstrap/lib/Carousel';
import CarouselItem from 'reactstrap/lib/CarouselItem';
import CarouselCaption from 'reactstrap/lib/CarouselCaption';

const items = [
    {
        src: 'https://images-na.ssl-images-amazon.com/images/G/01/AMAZON_FASHION/2017/EDITORIAL/WINTER_2/GATEWAY/DESKTOP/HERO_W_S_Pumps_1x._CB490669730_.jpg',
        altText: 'Slide 1',
        caption: 'Slide 1'
    },
    {
        src: 'https://www.konga.com/media/upload/slideshow/items/2018-02-05-03-02-301020400712.png',
        altText: 'Slide 2',
        caption: 'Slide 2'
    },
    {
        src: 'https://www.konga.com/media/upload/slideshow/items/2018-02-12-10-02-19584145862.png',
        altText: 'Slide 3',
        caption: 'Slide 3'
    }
];

class Carouselroll extends Component {
    constructor(props) {
        super(props);
        this.state = { activeIndex: 0 };
        this.next = this.next.bind(this);
        this.previous = this.previous.bind(this);
        this.goToIndex = this.goToIndex.bind(this);
        this.onExiting = this.onExiting.bind(this);
        this.onExited = this.onExited.bind(this);
    }

    onExiting() {
        this.animating = true;
    }

    onExited() {
        this.animating = false;
    }

    next() {
        if (this.animating) return;
        const nextIndex = this.state.activeIndex === items.length - 1 ? 0 : this.state.activeIndex + 1;
        this.setState({ activeIndex: nextIndex });
    }

    previous() {
        if (this.animating) return;
        const nextIndex = this.state.activeIndex === 0 ? items.length - 1 : this.state.activeIndex - 1;
        this.setState({ activeIndex: nextIndex });
    }

    goToIndex(newIndex) {
        if (this.animating) return;
        this.setState({ activeIndex: newIndex });
    }

    render() {
        const { activeIndex } = this.state;

        const slides = items.map((item) => {
            return (
                <CarouselItem
                    onExiting={this.onExiting}
                    onExited={this.onExited}
                    key={item.src}
                >
                    <img src={item.src} alt={item.altText} />
                    <CarouselCaption captionText={item.caption} captionHeader={item.caption} />
                </CarouselItem>
            );
        });

        return (
            <Carousel
                activeIndex={activeIndex}
                next={this.next}
                previous={this.previous}
            >
                <CarouselIndicators items={items} activeIndex={activeIndex} onClickHandler={this.goToIndex} />
                {slides}
                <CarouselControl direction="prev" directionText="Previous" onClickHandler={this.previous} />
                <CarouselControl direction="next" directionText="Next" onClickHandler={this.next} />
            </Carousel>
        );
    }
}


export default Carouselroll;

