import React from 'react'
import Spinner from 'react-bootstrap/Spinner';
import { useTranslation } from "react-i18next";

type Props = {
    children?: React.ReactElement;
    isReady: boolean;
}

function LoadingScreen(props: Props) {
    const { t } = useTranslation();
    return (
        <>{
            props.isReady ?
                props.children :
                (<div className='d-flex justify-content-center m-1 p-1'>
                    <Spinner animation='border' role='status'>
                        <span className='visually-hidden'>
                            {t('loading')}
                        </span>
                    </Spinner>
                </div>)
        }
        </>
    )
}

export default LoadingScreen