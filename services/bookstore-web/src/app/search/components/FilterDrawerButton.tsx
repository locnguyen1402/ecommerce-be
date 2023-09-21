"use client";

import { useState } from "react";

import { Box, Button, Drawer } from "@mui/material";
import { FilterAltOutlined as FilterIcon } from "@mui/icons-material";

import FilterSection from "./FilterSection";

const FilterDrawerButton = () => {
  const [visible, setVisible] = useState(false);

  return (
    <>
      <Button
        startIcon={<FilterIcon />}
        sx={{
          textTransform: "capitalize",
          display: {
            md: "none",
          },
        }}
        onClick={() => setVisible(true)}
      >
        Filter
      </Button>
      <Drawer
        sx={{
          display: {
            md: "none",
          },
        }}
        anchor="right"
        open={visible}
        onClose={() => setVisible(false)}
      >
        <Box paddingX={3} paddingY={4}>
          <FilterSection />
        </Box>
      </Drawer>
    </>
  );
};

export default FilterDrawerButton;
