import SimpleWave from "components/waves/simpleWave.tsx";
import {Restaurant} from "ApiConst/ApiResponse.ts";

type Props = {
    restaurant: Restaurant
}
const BusinessCard = (props: Props) => {
    return (
        <div className="business-card">
            <div className="image" style={{backgroundImage: `url(${props.restaurant.imageUrl})`, backgroundPosition: "center"}}></div>
            <SimpleWave/>
            <div className="title"><b>{props.restaurant.name}</b></div>
            <div className="category">{props.restaurant.category.name}</div>
            <div className="rating">★★★★</div>
        </div>
    );
}

export default BusinessCard;