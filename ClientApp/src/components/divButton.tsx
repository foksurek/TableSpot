
type DivButtonProps = {
    onClick?: () => void;
    children?: React.ReactNode;
    outline?: boolean;
    isButton?: boolean;
    isDisabled?: boolean;
}

const DivButton = (props: DivButtonProps) => {
    return (
        !props.isButton ?
            <div className={props.outline ? "divButtonOutlined" : "divButton"}
                 onClick={props.onClick}
            >
                {props.children}
            </div>
            :
            <button type="submit" className={props.outline ? "divButtonOutlined" : "divButton"}
                    onClick={props.onClick}
                    disabled={props.isDisabled}
            >
                {props.children}
            </button>

    );
}

export default DivButton;

