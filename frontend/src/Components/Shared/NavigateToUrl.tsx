import React, { ReactElement, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function NavigateToUrl(props: { url: string }): ReactElement {
  const navigate = useNavigate();
  useEffect(() => {
    navigate(props.url);
  }, []);
  return <></>;
}

export default NavigateToUrl;
