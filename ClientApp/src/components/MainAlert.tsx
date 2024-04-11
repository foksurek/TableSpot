import {Alert} from "@mui/material";
import {ErrorVariants} from "objects/errorVariants.ts";

type Props = {
    Variant: string,
    Message: string
}

const MainAlert = (props: Props) => {
    return (
        <>
            <div className="alert">
                {props.Variant === ErrorVariants.success && <Alert severity="success">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.info && <Alert severity="info">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.warning && <Alert severity="warning">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.error && <Alert severity="error">{props.Message}</Alert>}
                
            </div>
        </>
    )
}

export default MainAlert