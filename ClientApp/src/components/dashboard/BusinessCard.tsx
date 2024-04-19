import { restaurant } from "ApiConst/ApiResponse";
import SimpleWave from "../waves/simpleWave.tsx";

type Props = {
    restaurant: restaurant
}
const BusinessCard = (props: Props) => {
    return (
        <div className="BusinessCard">
            <div className="image" style={{backgroundImage: `url(${props.restaurant.imageUrl})`, backgroundPosition: "center"}}></div>
            <SimpleWave/>
            <div className="title">{props.restaurant.name}</div>
            <div className="category">{props.restaurant.category.name}</div>
            <div className="rating">★★★★</div>
        </div>
    );
}

export default BusinessCard;