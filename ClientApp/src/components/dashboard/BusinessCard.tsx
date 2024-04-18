import { restaurant } from "ApiConst/ApiResponse";

type Props = {
    restaurant: restaurant
}
const BusinessCard = (props: Props) => {
  return (
    <div className="card">
      {props.restaurant.name}
    </div>
  );
}

export default BusinessCard;