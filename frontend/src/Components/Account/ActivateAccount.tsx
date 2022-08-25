import React from 'react';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { Button, Card, Col, Row } from 'react-bootstrap';
import { Navigate } from 'react-router-dom';
import RegisterStudent from './RegisterStudent';
import RegisterTeacher from './RegisterTeacher';

const mapStateToProps = (state: any) => ({
    isUserLoggedIn: state.common.isLoggedIn,
    isUserActivated: state.common.session?.roles.length != 0
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface ActivateAccountProps{
    t: any,
    isUserLoggedIn: boolean;
    isUserActivated: boolean;
}

interface ActivateAccountState{
    role?: string
}

class ActivateAccount extends React.Component<ActivateAccountProps, ActivateAccountState> {
    constructor(props: ActivateAccountProps) {
        super(props);
        this.state = {
            role: undefined
        };
    }
    render(): React.ReactNode {
        const { t } = this.props;
        return (
            <>
     
                <Card className="m-3">
                    <Card.Body>
                        <div>
                            <div className='display-6'>
                                {t('activateToUseGradebook')}
                            </div>
                            <Row className='m-3 p-3'>
                                <Col/>
                                <Col className='text-center'>
                                    {
                                        !this.state.role && 
                                        <>
                                            <Row className='text-center'>
                                                <div>
                                                    {t('iAmA...')}
                                                </div>
                                            </Row>
                                            <Row>
                                                <Col>
                                                    <Button className='fs-3 m-3 p-3' 
                                                        variant='outline-secondary'
                                                        onClick={()=>this.setState({
                                                            ...this.state,
                                                            role: 'student'
                                                        })}>
                                                        {t("Student")}
                                                    </Button>
                                                    <Button className='fs-3 m-3 p-3' 
                                                        variant='outline-secondary' 
                                                        onClick={()=>this.setState({
                                                            ...this.state,
                                                            role: 'teacher'
                                                        })}>
                                                        {t("Teacher")}
                                                    </Button>
                                                </Col>
                                            </Row>
                                        </>
                                    }
                                    {
                                        this.state.role === 'teacher' && 
                                        <>
                                            <RegisterTeacher
                                                goBackHandler={()=>this.setState({
                                                    ...this.state,
                                                    role: undefined
                                                })}
                                            />
                                        </>
                                    }
                                    {
                                        this.state.role === 'student' && 
                                        <>
                                            <RegisterStudent
                                                goBackHandler={()=>this.setState({
                                                    ...this.state,
                                                    role: undefined
                                                })}
                                            />
                                        </>
                                    }
                                    
                                </Col>
                                <Col/>
                            </Row>
                        </div>
                    </Card.Body>
                </Card>
            </>
        );
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(ActivateAccount));
