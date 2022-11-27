import React, { ReactElement } from 'react';
import { Badge } from 'react-bootstrap';
interface PersonProps {
  name: string;
  surname: string;
  className?: string;
}
const PersonSmall = (props: PersonProps): ReactElement => {
  return (
    <Badge className={`m-1 p-2 ${props.className ?? ''}`}>
      <span>{`${props.name} ${props.surname}`}</span>
    </Badge>
  );
};
export default PersonSmall;
