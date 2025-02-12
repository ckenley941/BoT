import PropTypes from 'prop-types';
import Box from '@mui/material/Box';

export default function TabPanel(props) {
    const { children, value, index, ...other } = props;
  
    return (
    <div role="tabpanel" hidden={value !==index} id={`full-width-tabpanel-${index}`}
      aria-labelledby={`full-width-tab-${index}`} {...other}>
      {value === index && (
      <Box sx={{ p: 3 }}>
        <div>{children}</div>
      </Box>
      )}
    </div>
    );
    }
  
  TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.number.isRequired,
  };