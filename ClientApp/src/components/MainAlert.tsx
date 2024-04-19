import {Alert} from "@mui/material";
import {ErrorVariants} from "objects/errorVariants.ts";

type Props = {
    Variant: string,
    Message: string
    setError: (value: boolean) => void;
}

const MainAlert = (props: Props) => {
    return (
        <>
            <div className="alert">
                {props.Variant === ErrorVariants.success && <Alert variant="filled" onClose={() => {props.setError(false)}} severity="success">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.info && <Alert variant="filled" onClose={() => {props.setError(false)}} severity="info">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.warning && <Alert variant="filled" onClose={() => {props.setError(false)}} severity="warning">{props.Message}</Alert>}
                {props.Variant === ErrorVariants.error && <Alert variant="filled" onClose={() => {props.setError(false)}} severity="error">{props.Message}</Alert>}
                
            </div>
        </>
    )
}

export default MainAlert