import * as React from 'react';
import { connect } from 'react-redux';
import axios from 'axios';


export class About extends React.Component<any, any> {
    constructor(props: any) {
        super(props);

        this.state = {
            configuration: {}
        }
    }

    async componentDidMount() {
        let response = await axios.get("api/dependencies/configuration");
        this.setState({
            configuration: response.data
        });
    }

    public render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-12">
                        <i className="fab fa-2x fa-instagram" /> <h5> {this.state.configuration.secret} </h5>
                    </div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-12">
                        <i className="fab fa-2x fa-twitter" /> <h5>{this.state.configuration.appsettings} </h5>
                    </div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-12">
                        <i className="fab fa-2x fa-linkedin" /> <h5> {this.state.configuration.environment} </h5>
                    </div>
                </div>
            </div>
        );
    }
};

export default connect()(About);
