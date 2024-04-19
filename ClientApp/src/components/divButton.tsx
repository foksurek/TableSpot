
type DivButtonProps = {
    onClick?: () => void;
    children?: React.ReactNode;
    outline?: boolean;
}

const DivButton = (props: DivButtonProps) => {
    return (
        <div className={props.outline ? "divButtonOutlined" : "divButton"} onClick={props.onClick}>
            {props.children}
        </div>
    );
}

export default DivButton;

